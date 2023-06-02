using System;
using TMPro;
using UnityEngine;
using FunkyCode;
using Sirenix.OdinInspector;

namespace Managers
{
    [System.Serializable]
    public class LightCycleGradient
    {
        public Gradient gradient = new();
    }

    [System.Serializable]
    public class LightDayProperties
    {
        [Range(0, 360)]
        public float shadowOffset = 0;

        public AnimationCurve shadowHeight = new AnimationCurve();

        public AnimationCurve shadowAlpha = new AnimationCurve();  
    }
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private float timeMultiplier;

        [SerializeField] private float startHour;

        [SerializeField] private TextMeshProUGUI timeText;
    
        private DateTime currentTime;


        [Title("Time specific things")] [SerializeField]
        private float sunriseHour;
        [SerializeField]private float sunsetHour;
        private TimeSpan sunriseTime;
        private TimeSpan sunsetTime;


        [Title("Lighting")]
        public LightDayProperties dayProperties = new LightDayProperties();
        public LightCycleBuffer[] nightProperties = new LightCycleBuffer[1]; 
        
        
        // Start is called before the first frame update
        void Start()
        {
            currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
            sunsetTime = TimeSpan.FromHours(sunsetHour);
            sunriseTime = TimeSpan.FromHours(sunriseHour);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateTimeOfDay();
        }


        void UpdateTimeOfDay()
        {
            currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        
            timeText.SetText(currentTime.ToString("HH:mm"));
        }


        private void LateUpdate()
        {
            float sunLightRotation;

            
                var sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
                var timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

                double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;


                sunLightRotation = Mathf.Lerp(0, 360, (float) percentage);
                
                //Debug.Log($"Percentage: {percentage}");
                
                UpdateLighting((float)percentage,sunLightRotation);
        }


        private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
        {
            var difference = toTime - fromTime;

            if (difference.TotalSeconds < 0)
            {
                difference += TimeSpan.FromHours(24);
            }

            return difference;
        }

        void UpdateLighting(float percentage, float rotation)
        {
            
            
            var lightmapPresets = Lighting2D.Profile.lightmapPresets;

            if (lightmapPresets == null) return;

            
            // Day Lighting Properties
           float height = dayProperties.shadowHeight.Evaluate(percentage);
           float alpha = dayProperties.shadowAlpha.Evaluate(percentage);
           float direction = rotation + dayProperties.shadowOffset;
           
//
            if (height < 0.01f)
            {
                height = 0.01f;
            }
//
            if (alpha < 0)
            {
                alpha = 0;
            }

            Lighting2D.DayLightingSettings.height = height;
            Lighting2D.DayLightingSettings.ShadowColor.a = alpha;
            Lighting2D.DayLightingSettings.direction = direction;
            
            
            // Dynamic Properties
            for(int i = 0; i < nightProperties.Length; i++) {
                if (i >= lightmapPresets.list.Length) {
                    return;
                }

                LightCycleBuffer buffer = nightProperties[i];

                if (buffer == null) {
                    continue;
                }

                Color color = buffer.gradient.Evaluate(percentage);

                var lightmapPreset = lightmapPresets.list[i];
                lightmapPreset.darknessColor = color;
            }

        }
    }
}
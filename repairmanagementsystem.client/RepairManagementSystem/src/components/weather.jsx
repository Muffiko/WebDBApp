import { useEffect, useState } from 'react';

function Weather() {
  const [forecast, setForecast] = useState([]);

  useEffect(() => {
    fetch(`/api/weatherforecast`)
      .then((response) => response.json())
      .then((data) => setForecast(data));
  }, []);

  return (
    <div>
      <h1>Weather Forecast</h1>
      <ul>
        {forecast.map((weather, index) => (
          <li key={index}>
            {weather.date} - {weather.temperatureC}°C - {weather.summary}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default Weather;


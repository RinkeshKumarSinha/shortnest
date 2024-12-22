// src/components/ShortenUrl.js
import React, { useState } from 'react';
import axios from 'axios';
import './ShortenUrl.css';

const ShortenUrl = () => {
  const [url, setUrl] = useState('');
  const [expiryDuration, setExpiryDuration] = useState('');
  const [shortenedUrl, setShortenedUrl] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post('https://localhost:44392/api/Main/shorten', {
        url: url,
        expiryDuration: expiryDuration
      }, {
        headers: {
          'Content-Type': 'application/json'
        }
      });
      setShortenedUrl(response.data);
    } catch (error) {
      console.error('Error shortening URL:', error);
    }
  };

  const handleExpiryChange = (e) => {
    const selectedDuration = e.target.value;
    const maxDuration = 8 * 60 * 60; // 8 hours in seconds
    const durationInSeconds = selectedDuration.split(':').reduce((acc, time) => (60 * acc) + +time, 0);

    if (durationInSeconds <= maxDuration) {
      setExpiryDuration(selectedDuration);
    } else {
      alert('Expiry duration cannot be longer than 8 hours.');
    }
  };

  return (
    <div className="shorten-url-container">
      <h2>Shorten URL</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          value={url}
          onChange={(e) => setUrl(e.target.value)}
          placeholder="Enter URL"
          className="input-field"
        />
        <select value={expiryDuration} onChange={handleExpiryChange} className="input-field">
          <option value="">Select Expiry Duration</option>
          <option value="00:01:00">1 Minute</option>
          <option value="00:05:00">5 Minutes</option>
          <option value="00:10:00">10 Minutes</option>
          <option value="00:30:00">30 Minutes</option>
          <option value="01:00:00">1 Hour</option>
          <option value="02:00:00">2 Hours</option>
          <option value="04:00:00">4 Hours</option>
          <option value="06:00:00">6 Hours</option>
          <option value="08:00:00">8 Hours</option>
        </select>
        <button type="submit" className="submit-button">Shorten</button>
      </form>
      {shortenedUrl && (
        <div className="result">
          <h3>Shortened URL:</h3>
          <p>{shortenedUrl}</p>
        </div>
      )}
    </div>
  );
};

export default ShortenUrl;

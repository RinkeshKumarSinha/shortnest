// src/components/GetOriginalUrl.js
import React, { useState } from 'react';
import axios from 'axios';
import './GetOriginalUrl.css';

const GetOriginalUrl = () => {
  const [shortenedUrl, setShortenedUrl] = useState('');
  const [originalUrl, setOriginalUrl] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const encodedUrl = encodeURIComponent(shortenedUrl);
      const response = await axios.get(`https://localhost:44392/api/Main/${encodedUrl}`);
      setOriginalUrl(response.data);
    } catch (error) {
      console.error('Error retrieving original URL:', error);
    }
  };

  const handleRedirect = () => {
    window.open(originalUrl, '_blank');
  };

  return (
    <div className="get-original-url-container">
      <h2>Get Original URL</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          value={shortenedUrl}
          onChange={(e) => setShortenedUrl(e.target.value)}
          placeholder="Enter Shortened URL"
          className="input-field"
        />
        <button type="submit" className="submit-button">Get Original URL</button>
      </form>
      {originalUrl && (
        <div className="result">
          <h3>Original URL:</h3>
          <p>{originalUrl}</p>
          <button onClick={handleRedirect} className="redirect-button">Redirect</button>
        </div>
      )}
    </div>
  );
};

export default GetOriginalUrl;

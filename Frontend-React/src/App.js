// src/App.js
import React from 'react';
import ShortenUrl from './components/ShortenUrl';
import GetOriginalUrl from './components/GetOriginalUrl';
import Footer from './components/Footer';
import './App.css';

const App = () => {
  return (
    <div className="App">
      <header className="App-header">
        <h1>ShortNest URL Shortener</h1>
      </header>
      <main>
        <ShortenUrl />
        <GetOriginalUrl />
      </main>
      <Footer />
    </div>
  );
};

export default App;

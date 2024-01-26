import React, { useState, useEffect } from 'react';
import '../styles/waiting.css';

const WaitingMessage = () => {
  const [showMessage, setShowMessage] = useState(true);

  return (
    <div className="waiting-message">
      {showMessage && <span>Waiting for opponent...</span>}
    </div>
  );
};

export default WaitingMessage;


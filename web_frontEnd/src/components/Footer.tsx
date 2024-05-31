import React from "react";

import "./footer.css"; // Import the CSS file
const Footer: React.FC = () => {
  return (
    <footer className="footer">
      <div className="col-md-1">
        <p>&copy; 2024 Example Company. All rights reserved.</p>
      </div>
      <div className="col-md-6">
        <ul className="footer-links">
          <li>
            <a href="/">Home</a>
          </li>
          <li>
            <a href="/about">About</a>
          </li>
          <li>
            <a href="/contact">Contact</a>
          </li>
          <li>
            <a href="/privacy-policy">Privacy Policy</a>
          </li>
        </ul>
      </div>
    </footer>
  );
};

export default Footer;

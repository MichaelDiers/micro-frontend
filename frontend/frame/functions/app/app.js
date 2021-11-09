const express = require('express');

const controller = require('./controller/controller');
const middleware = require('./middleware/middleware');
const router = require('./router/router');

const initialize = () => {
  const app = express();
  return app;
};

module.exports = initialize;

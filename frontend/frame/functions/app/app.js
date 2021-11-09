const express = require('express');

// const controller = require('./controller/controller');
// const middleware = require('./middleware/middleware');
// const router = require('./router/router');

const initialize = (config = {}) => {
  const {
    view: {
      engine: viewEngine,
      folder: viewFolder,
    },
  } = config;

  const app = express();

  app.set('views', viewFolder);
  app.set('view engine', viewEngine);

  return app;
};

module.exports = initialize;

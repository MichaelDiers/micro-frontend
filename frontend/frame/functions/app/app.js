const express = require('express');

// const controller = require('./controller/controller');
// const middleware = require('./middleware/middleware');
const router = require('./router/router');

const initialize = (config = {}) => {
  const {
    view: {
      engine: viewEngine,
      folder: viewFolder,
    },
  } = config;

  const mainRouter = express.Router();
  mainRouter.use('/', router.frame());

  const app = express();

  app.set('views', viewFolder);
  app.set('view engine', viewEngine);

  app.use('/', mainRouter);
  return app;
};

module.exports = initialize;

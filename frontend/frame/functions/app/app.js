const express = require('express');

const controller = require('./controller/controller');
const middleware = require('./middleware/middleware');
const router = require('./router/router');

const routeHandlerHelper = require('./helper/route-handler');

const initialize = (config = {}) => {
  const {
    route: {
      error: errorRoute,
      frame: frameRoute,
    },
    view: {
      engine: viewEngine,
      folder: viewFolder,
    },
  } = config;

  const routeHandler = routeHandlerHelper({ config });

  const mainRouter = express.Router();
  middleware.pug({ router: mainRouter });
  mainRouter.use(errorRoute, router.error({ controller: controller.error(), routeHandler }));
  mainRouter.use(frameRoute, router.frame({ controller: controller.frame(), routeHandler }));

  const app = express();

  app.set('views', viewFolder);
  app.set('view engine', viewEngine);

  app.use('/', middleware.base());

  app.use('/', mainRouter);

  middleware.error({ config, router: app });
  return app;
};

module.exports = initialize;

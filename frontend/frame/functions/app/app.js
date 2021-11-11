const express = require('express');

const controller = require('./controller/controller');
const middleware = require('./middleware/middleware');
const router = require('./router/router');

const routeHandlerHelper = require('./helper/route-handler');

const initialize = (config = {}) => {
  const {
    statics: {
      folder: staticFolder,
      route: staticRoute,
    },
    route: {
      error: errorRoute,
      frame: frameRoute,
      license: licenseRoute,
      pictureCredit: pictureCreditRoute,
    },
    view: {
      engine: viewEngine,
      folder: viewFolder,
    },
  } = config;

  const routeHandler = routeHandlerHelper({ config });

  const mainRouter = express.Router();

  const statics = express.static(staticFolder, { index: false });
  mainRouter.use(staticRoute, statics);

  middleware.pug({ config, router: mainRouter });
  mainRouter.use(errorRoute, router.error({ controller: controller.error(), routeHandler }));
  mainRouter.use(frameRoute, router.frame({ controller: controller.frame(), routeHandler }));
  mainRouter.use(licenseRoute, router.license({ controller: controller.license(), routeHandler }));
  mainRouter.use(
    pictureCreditRoute,
    router.pictureCredit({ controller: controller.pictureCredit(), routeHandler }),
  );

  const app = express();
  middleware.base({ router: app });

  app.set('views', viewFolder);
  app.set('view engine', viewEngine);

  app.use('/', mainRouter);

  middleware.error({ config, router: app });

  return app;
};

module.exports = initialize;

const express = require('express');

const controller = require('./controller/controller');
const middleware = require('./middleware/middleware');
const router = require('./router/router');

const initialize = (config = {}) => {
  const {
    statics: {
      folder: staticFolder,
      route: staticRoute,
    },
    route: {
      account: accountRoute,
      version: versionRoute,
    },
    view: {
      engine: viewEngine,
      folder: viewFolder,
    },
  } = config;

  const mainRouter = express.Router();

  const statics = express.static(staticFolder, { index: false });
  mainRouter.use(staticRoute, statics);
  middleware.language({ router: mainRouter });
  mainRouter.use(middleware.pug({ config }));

  mainRouter.use(`(/de|/en)?${accountRoute}`, router.account({ controller: controller.account() }));
  mainRouter.use(`(/de|/en)?${versionRoute}`, router.version({ controller: controller.version() }));

  const app = express();
  middleware.base({ config, router: app });

  app.set('views', viewFolder);
  app.set('view engine', viewEngine);

  app.use('/', mainRouter);

  return app;
};

module.exports = initialize;

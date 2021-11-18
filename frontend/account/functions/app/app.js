const express = require('express');

const controller = require('./controller/controller');
const language = require('./language/language');
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

  const urlPrefix = `(/${language.supported.join('|/')})?`;
  mainRouter.use(`${urlPrefix}${accountRoute}`, router.account({ controller: controller.account() }));
  mainRouter.use(`${urlPrefix}${versionRoute}`, router.version({ controller: controller.version() }));

  const app = express();
  middleware.base({ config, router: app });

  app.set('views', viewFolder);
  app.set('view engine', viewEngine);

  app.use('/', mainRouter);

  return app;
};

module.exports = initialize;

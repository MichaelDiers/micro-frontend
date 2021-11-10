const firebaseFunctions = require('firebase-functions');

const initialize = () => {
  const {
    baseurl: baseUrl,
  } = firebaseFunctions.config().frame;

  const config = {
    logger: firebaseFunctions.logger.error,
    route: {
      error: '/error',
      frame: '/frame',
      license: '/license',
    },
    statics: {
      folder: 'app/public',
      route: '/public',
    },
    url: {
    },
    view: {
      engine: 'pug',
      folder: './app/views',
    },
  };

  config.url.error404 = `${baseUrl}${config.route.error}/404`;
  config.url.error500 = `${baseUrl}${config.route.error}/500`;
  config.url.publicUrl = `${baseUrl}${config.statics.route}`;
  return config;
};

module.exports = initialize;

const firebaseFunctions = require('firebase-functions');

const initialize = () => {
  const {
    baseurl: baseUrl,
  } = firebaseFunctions.config().account;

  const config = {
    logger: firebaseFunctions.logger.error,
    route: {
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

  config.url.publicUrl = `${baseUrl}${config.statics.route}`;
  return config;
};

module.exports = initialize;

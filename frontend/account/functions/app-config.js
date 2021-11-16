const firebaseFunctions = require('firebase-functions');

const initialize = () => {
  const {
    baseurl: baseUrl,
    corsfeframe: corsFeFrame,
    version,
  } = firebaseFunctions.config().account;

  const config = {
    logger: firebaseFunctions.logger.error,
    route: {
      account: '/account',
      version: '/version',
    },
    statics: {
      folder: 'app/public',
      route: '/public',
    },
    url: {
      corsFeFrame,
    },
    version,
    view: {
      engine: 'pug',
      folder: './app/views',
    },
  };

  config.url.publicUrl = `${baseUrl}${config.statics.route}`;
  return config;
};

module.exports = initialize;

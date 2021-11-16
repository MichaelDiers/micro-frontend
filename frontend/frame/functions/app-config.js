const firebaseFunctions = require('firebase-functions');

const initialize = () => {
  const {
    accountfehost: accountFeHost,
    accountfepath: accountFePath,
    baseurl: baseUrl,
    version,
  } = firebaseFunctions.config().frame;

  const config = {
    cssFiles: [
      'client.min.css',
    ],
    jsFiles: [
      'client.min.js',
    ],
    logger: firebaseFunctions.logger.error,
    route: {
      account: '/account',
      error: '/error',
      frame: '/frame',
      license: '/license',
      pictureCredit: '/pictureCredit',
      version: '/version',
    },
    statics: {
      folder: 'app/public',
      route: '/public',
    },
    url: {
      accountFeHost,
      licenses: [],
    },
    version,
    view: {
      engine: 'pug',
      folder: './app/views',
    },
  };

  config.url.error404 = `${baseUrl}${config.route.error}/404`;
  config.url.error500 = `${baseUrl}${config.route.error}/500`;
  config.url.publicUrl = `${baseUrl}${config.statics.route}`;
  config.url.jsFiles = config.jsFiles.map((jsFile) => `${config.url.publicUrl}/${jsFile}`);
  config.url.accountFePublic = `${accountFeHost}${accountFePath}/public`;
  config.url.jsFiles.push(`${config.url.accountFePublic}/client.min.js`);
  config.url.cssFiles = config.cssFiles.map((cssFile) => `${config.url.publicUrl}/${cssFile}`);
  config.url.licenseUrl = `${baseUrl}${config.route.license}`;
  config.url.pictureCreditUrl = `${baseUrl}${config.route.pictureCredit}`;
  config.url.home = `${baseUrl}${config.route.frame}`;
  config.url.versionUrl = `${baseUrl}${config.route.version}`;
  return config;
};

module.exports = initialize;

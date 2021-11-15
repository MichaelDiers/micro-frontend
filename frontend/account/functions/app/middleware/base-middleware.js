const compression = require('compression');
const express = require('express');
const helmet = require('helmet');

/**
 * Initialize basic middleware used for all routes, like helmet and compression.
 * @param {object} options Options including an optional router.
 * @returns The given router from options or a new express.Router.
 */
const initialize = (options = {}) => {
  const {
    config: {
      url: {
        corsFeFrame,
      },
    },
    router = express.Router(),
  } = options;
  router.use(helmet());

  // cors: https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS
  router.use((req, res, next) => {
    res.set('Access-Control-Allow-Origin', corsFeFrame);
    res.set('Access-Control-Expose-Headers', 'Content-Type, Authorization');
    res.set('Access-Control-Allow-Credentials', 'true');
    res.set('Access-Control-Allow-Methods', 'GET, POST');
    next();
  });

  router.use(compression());
  router.use(express.urlencoded({ extended: false }));
  router.use(express.json());

  return router;
};

module.exports = initialize;

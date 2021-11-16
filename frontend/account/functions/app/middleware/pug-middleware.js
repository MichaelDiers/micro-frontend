const express = require('express');

/**
 * Initialize options used for pug processing. The options are set to res.locals.pugOptions.
 * @param {object} options Options contain an optional router and the application configuration.
 * @returns The given router from the options or a new express.Router.
 */
const initialize = (options = {}) => {
  const {
    config: {
      version,
    },
    router = express.Router(),
  } = options;

  router.use((req, res, next) => {
    const pugOptions = {
      version,
    };

    res.locals.pugOptions = pugOptions;
    next();
  });

  return router;
};

module.exports = initialize;

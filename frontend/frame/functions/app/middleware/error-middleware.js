const express = require('express');

/**
 * Initialize the middleware for handling 404 and 500 errors.
 * @param {object} options Options contains an optional router and the application configuration.
 * @returns The given router from options or a new express.Router.
 */
const initialize = (options = {}) => {
  const {
    config: {
      logger,
      url: {
        error404,
        error500,
      },
    },
    router = express.Router(),
  } = options;

  // handle errors that occured during processing.
  router.use(async (
    err,
    req,
    res,
    next, // eslint-disable-line no-unused-vars
  ) => {
    logger(err);
    res.redirect(303, error500);
  });

  // handle unknown routes.
  router.use(async (req, res, next) => { // eslint-disable-line no-unused-vars
    res.redirect(303, error404);
  });

  return router;
};

module.exports = initialize;

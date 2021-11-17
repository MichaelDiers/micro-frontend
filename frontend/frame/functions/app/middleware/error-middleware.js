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
      route: {
        error,
      },
      url: {
        baseUrl,
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
    let url;
    if (res.locals.lang) {
      url = `${baseUrl}/${res.locals.lang}${error}/500`;
    } else {
      url = `${baseUrl}${error}/500`;
    }

    res.redirect(303, url);
  });

  // handle unknown routes.
  router.use(async (req, res, next) => { // eslint-disable-line no-unused-vars
    let url;
    if (res.locals.lang) {
      url = `${baseUrl}/${res.locals.lang}${error}/404`;
    } else {
      url = `${baseUrl}${error}/404`;
    }

    res.redirect(303, url);
  });

  return router;
};

module.exports = initialize;

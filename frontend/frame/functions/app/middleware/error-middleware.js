const express = require('express');

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

  router.use(async (
    err,
    req,
    res,
    next, // eslint-disable-line no-unused-vars
  ) => {
    logger(err);
    res.redirect(303, error500);
  });

  router.use(async (req, res, next) => { // eslint-disable-line no-unused-vars
    res.redirect(303, error404);
  });

  return router;
};

module.exports = initialize;

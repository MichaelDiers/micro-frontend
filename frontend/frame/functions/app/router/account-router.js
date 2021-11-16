const express = require('express');

/**
 * Initialize the account router.
 * @param {object} options An object containing the paramter to initialize the router.
 * @returns A new express.Router or the given router from the options.
 */
const initialize = (options = {}) => {
  const {
    controller,
    routeHandler,
    router = express.Router(),
  } = options;

  router.get('/*', async (req, res) => routeHandler(res, controller.index(req.originalUrl)));

  return router;
};

module.exports = initialize;

const express = require('express');

/**
 * Initialize the routes for error handling.
 * @param {object} options Options contains the used controller,
 *  routeHandler adn an optional router.
 * @returns The router from the options or a new express.Router.
 */
const initialize = (options = {}) => {
  const {
    controller,
    routeHandler,
    router = express.Router(),
  } = options;

  // handle 404 errors
  router.get('/404', async (req, res) => routeHandler(res, controller.error404(), false));

  // handle 500 errors
  router.get('/500', async (req, res) => routeHandler(res, controller.error500(), false));

  return router;
};

module.exports = initialize;

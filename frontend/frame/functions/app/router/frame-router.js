const express = require('express');

/**
 * Initialize the main entry route of the application.
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

  router.get('/', async (req, res) => routeHandler(res, controller.index()));

  return router;
};

module.exports = initialize;

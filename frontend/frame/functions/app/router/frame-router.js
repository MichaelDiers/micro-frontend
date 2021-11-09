const express = require('express');

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

const express = require('express');

const initialize = (options = {}) => {
  const {
    controller,
    routeHandler,
    router = express.Router(),
  } = options;

  router.get('/404', async (req, res) => routeHandler(res, controller.error404(), false));

  router.get('/500', async (req, res) => routeHandler(res, controller.error500(), false));

  return router;
};

module.exports = initialize;

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
    router = express.Router(),
  } = options;
  router.use(helmet());
  // https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS
  router.use((req, res, next) => {
    res.set('Access-Control-Allow-Origin', 'https://us-central1-frame-25f3b.cloudfunctions.net');
    // Vary: Origin
    res.set('Access-Control-Expose-Headers', 'Content-Type, Authorization');
    res.set('Access-Control-Allow-Credentials', 'true');
    res.set('Access-Control-Allow-Methods', 'GET, POST');
    next();
  });
  /**
  router.use(cors({
    origin: 'https://us-central1-frame-25f3b.cloudfunctions.net',
    methods: 'GET',
    allowedHeaders: ['Content-Type', 'Authorization'],
    credentials: true,
  }));
  */
  router.use(compression());
  router.use(express.urlencoded({ extended: false }));
  router.use(express.json());

  return router;
};

module.exports = initialize;

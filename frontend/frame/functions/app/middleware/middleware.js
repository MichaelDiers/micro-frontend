const base = require('./base-middleware');
const error = require('./error-middleware');
const pug = require('./pug-middleware');

module.exports = {
  base,
  error,
  pug,
};

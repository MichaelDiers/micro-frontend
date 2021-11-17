const base = require('./base-middleware');
const error = require('./error-middleware');
const language = require('./language-middleware');
const pug = require('./pug-middleware');

module.exports = {
  base,
  error,
  language,
  pug,
};

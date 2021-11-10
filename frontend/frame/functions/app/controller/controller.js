/**
 * Export all available controllers.
 */

const error = require('./error-controller');
const frame = require('./frame-controller');
const license = require('./license-controller');

module.exports = {
  error,
  frame,
  license,
};

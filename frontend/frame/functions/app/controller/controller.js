/**
 * Export all available controllers.
 */

const error = require('./error-controller');
const frame = require('./frame-controller');
const license = require('./license-controller');
const pictureCredit = require('./picture-credit-controller');

module.exports = {
  error,
  frame,
  license,
  pictureCredit,
};

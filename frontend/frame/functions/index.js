const functions = require('firebase-functions');

const app = require('./app/app');
const appConfig = require('./app-config');

exports.frame = functions.https.onRequest(app(appConfig()));

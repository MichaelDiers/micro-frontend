const de = require('./language-de.json');
const en = require('./language-en.json');

const translations = (language) => {
  if (language === 'de') {
    return de;
  }

  if (language === 'en') {
    return en;
  }

  return {};
};

const translate = (language, key) => {
  try {
    return translations(language)[key];
  } catch {
    return 'unknown';
  }
};

module.exports = {
  de: 'de',
  default: 'de',
  en: 'en',
  supported: ['de', 'en'],
  translate,
  translations,
};

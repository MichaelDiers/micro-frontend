const formValidation = (rootElement, ...cssSelectors) => { // eslint-disable-line no-unused-vars
  let valid = true;
  cssSelectors.forEach((cssSelector) => {
    const element = rootElement.querySelector(cssSelector);
    const errorElement = rootElement.querySelector(`${cssSelector}Error`);
    if (!element || !errorElement || !element.checkValidity()) {
      valid = false;
      if (errorElement) {
        errorElement.innerText = element.validationMessage;
      }

      if (element) {
        element.classList.add('error');
      }
    } else {
      errorElement.innerText = null;
      element.classList.remove('error');
    }
  });

  return valid;
};

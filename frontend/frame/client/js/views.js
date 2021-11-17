/**
 * Search for frame-request-by attributed elements and raise the event for
 * replacing the innerHTML of the element. The replacement has to be handled
 * by a different micro frontend.
 */
const frameReplaceElements = async () => {
  const frameRequestAttributeName = 'frame-request-by';
  const baseAddress = `${window.location.href.split('framefe')[0]}framefe`; // eslint-disable-line no-undef
  document.addEventListener('DOMContentLoaded', () => { // eslint-disable-line no-undef
    const elements = document.querySelectorAll(`[${frameRequestAttributeName}]`); // eslint-disable-line no-undef
    if (elements && elements.forEach) {
      elements.forEach((element) => {
        const frameRequestAttribute = element.getAttribute(frameRequestAttributeName);
        element.removeAttribute(frameRequestAttributeName);
        element.dispatchEvent(
          new CustomEvent( // eslint-disable-line no-undef
            frameRequestAttribute,
            {
              bubbles: true,
              cancelable: true,
              detail: baseAddress,
            },
          ),
        );
      });
    }
  });
};

frameReplaceElements().catch(handleError); // eslint-disable-line no-undef

document.addEventListener('accountIndexViewRequest', (e) => { // eslint-disable-line no-undef
  e.preventDefault();
  const accountIndexElement = document.querySelector('.account-index'); // eslint-disable-line no-undef
  accountIndexElement.innerHTML = e.detail.view;
  e.detail.callback().catch(handleError); // eslint-disable-line no-undef
});

document.addEventListener('mainViewRequest', (e) => { // eslint-disable-line no-undef
  e.preventDefault();
  const mainElement = document.getElementsByTagName('main'); // eslint-disable-line no-undef
  if (mainElement && mainElement[0]) {
    mainElement[0].innerHTML = e.detail.view;
  }
});

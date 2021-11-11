const loadPictureCredits = async () => {
  const picturesElement = document.getElementById('pictures'); // eslint-disable-line no-undef
  if (!picturesElement) {
    return;
  }

  const promises = address.pictureCredits.map((url) => apiCall(url, 'GET')); // eslint-disable-line no-undef
  const values = await Promise.all(promises);
  values.forEach((pictureCredits, i) => {
    const url = address.pictureCredits[i].substr(0, address.pictureCredits[i].lastIndexOf('/')); // eslint-disable-line no-undef
    if (pictureCredits && pictureCredits.forEach) {
      pictureCredits.forEach(({ name, author }) => {
        const parent = addDivElement({ // eslint-disable-line no-undef
          parent: picturesElement,
          classes: ['grid-col-2'],
        });

        addNewElement({ // eslint-disable-line no-undef
          parent,
          elementName: 'img',
          attributes: [
            { name: 'src', value: `${url}/${name}` },
            { name: 'alt', value: name },
            { name: 'width', value: 150 },
            { name: 'height', value: 50 },
          ],
        });

        addDivElement({ // eslint-disable-line no-undef
          parent,
          innerText: `${name} - ${author}`,
        });
      });
    }
  });
};

if (document.getElementById('id_ca156b87aaaf42fd9ff1ccecda7828c2')) { // eslint-disable-line no-undef
  loadPictureCredits().catch(handleError); // eslint-disable-line no-undef
}

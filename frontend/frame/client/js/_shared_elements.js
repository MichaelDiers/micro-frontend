const addNewElement = (options) => {
  const {
    attributes,
    classes,
    parent,
    elementName,
    innerHtml,
    innerText,
  } = options;

  const element = document.createElement(elementName); // eslint-disable-line no-undef

  if (attributes) {
    attributes.forEach((attr) => {
      const { name, value } = attr;
      element.setAttribute(name, value);
    });
  }

  if (classes) {
    classes.forEach((cls) => element.classList.add(cls));
  }

  if (innerHtml) {
    element.innerHTML = innerHtml;
  }

  if (innerText || innerText === 0) {
    element.innerText = innerText;
  }

  parent.appendChild(element);

  return element;
};

const addDivElement = (options) => { // eslint-disable-line no-unused-vars
  const refOptions = options;
  refOptions.elementName = 'div';
  return addNewElement(refOptions);
};

const addSelectElement = (options) => { // eslint-disable-line no-unused-vars
  const {
    parent,
    labelAttributes = [],
    id,
    label,
    selectAttributes = [],
    options: selectOptions,
  } = options;
  const labelAttributesRef = labelAttributes;
  labelAttributes.push({ name: 'for', value: id });
  const labelElement = addNewElement({
    parent,
    elementName: 'label',
    attributes: labelAttributesRef,
    innerText: label,
  });

  const selectAttributesRef = selectAttributes;
  selectAttributesRef.push({ name: 'id', value: id });
  selectAttributesRef.push({ name: 'name', value: id });
  const selectElement = addNewElement({
    parent,
    elementName: 'select',
    attributes: selectAttributesRef,
  });

  if (selectOptions) {
    selectOptions.forEach(({ name, value, selected }) => {
      const attributes = [{ name: 'value', value }];
      if (selected) {
        attributes.push({ name: 'selected', value: 'selected' });
      }

      addNewElement({
        parent: selectElement,
        elementName: 'option',
        innerText: name,
        attributes,
      });
    });
  }

  return { labelElement, selectElement };
};

const addSubmitElement = (options) => { // eslint-disable-line no-unused-vars
  const { attributes = [], parent, value } = options;
  const attributesRef = attributes;
  attributesRef.push({ name: 'type', value: 'submit' });
  attributesRef.push({ name: 'value', value });
  return addNewElement({
    parent,
    elementName: 'input',
    attributes: attributesRef,
  });
};

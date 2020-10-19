function filterNestedFields(id, url, fieldId, callback = null) {
    const field = document.querySelector(fieldId);
    if (id) {
        fetch(`${url}=${id}`)
            .then(blob => blob.json())
            .then(data => {
                const items = document.querySelectorAll(`${fieldId} > option`);

                items.forEach(item => {
                    item.remove();
                });

                const defaultElement = document.createElement('option');
                defaultElement.value = '';
                defaultElement.text = '---------';

                field.append(defaultElement);

                data.forEach(d => {
                    const element = document.createElement('option');
                    element.value = d.Id;
                    element.text = d.Nome;
                    field.append(element);
                });

                field.removeAttribute('disabled');
            })
            .catch(() => alert('Erro ao filtrar as informações, tente novamente. Se o problema persistir, atualize a página.'));

        if (callback) {
            callback();
        }

    } else {
        field.value = '';
        field.setAttribute('disabled', true);
    }
}
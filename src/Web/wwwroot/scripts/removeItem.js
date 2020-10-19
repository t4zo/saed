function removeItem(id) {
    if (confirm('Tem certeza que deseja remover esse item?')) {
        document.querySelector(`.form-delete-${id}`).submit();
    }
}
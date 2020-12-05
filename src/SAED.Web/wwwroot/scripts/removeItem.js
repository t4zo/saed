function removeItem(id1, id2, id3) {
    let id = `.form-delete-${id1}`;

    if (id2) {
        id += `-${id2}`;
    }

    if (id3) {
        id += `-${id3}`;
    }

    if (confirm("Tem certeza que deseja remover esse item?")) {
        document.querySelector(id).submit();
    }
}
function smartSelect({
    available,
    chosen = [],
    mountNode = '#smartSelect'
}) {
    const app = Vue.createApp({
        data() {
            return {
                searchAvailableField: '',
                searchChosenField: '',
                available,
                availableSearch: [],
                availableSelected: [],
                chosen,
                chosenSelected: [],
                chosenSearch: [],
            }
        },
        methods: {
            searchAvailable(event = null, value) {
                if (event) {
                    this.searchAvailableField = event.target.value.toLowerCase();
                } else {
                    this.searchAvailableField = value.toLowerCase();
                }

                const availableSearchLowerCase = this.availableSearch.map(availSearch => availSearch.toLowerCase());

                this.availableSearch = this.available
                    .filter(avail => avail.toLowerCase().includes(this.searchAvailableField) && !availableSearchLowerCase.includes(this.searchAvailableField));
            },
            searchChosen(event = null, value) {
                if (event) {
                    this.searchChosenField = event.target.value.toLowerCase();
                } else {
                    this.searchChosenField = value.toLowerCase();
                }
                const chosenSearchLowerCase = this.chosenSearch.map(chosSearch => chosSearch.toLowerCase());

                this.chosenSearch = this.chosen
                    .filter(chos => chos.toLowerCase().includes(this.searchChosenField) && !chosenSearchLowerCase.includes(this.searchChosenField));
            },
            add() {
                if (this.availableSelected.length >= 1) {
                    this.availableSelected.forEach(availSelecionada => {
                        this.available = this.available.filter(x => x !== availSelecionada);
                    });

                    const availableSelected = JSON.parse(JSON.stringify(this.availableSelected));

                    availableSelected.forEach(availSelecionada => {
                        if (!this.chosen.includes(availSelecionada)) {
                            this.chosen = [...this.chosen, availSelecionada];
                        }
                    });
                }

                this.searchAvailable(null, this.searchAvailableField);
                this.searchChosen(null, this.searchChosenField);
            },
            addAll() {
                if (this.availableSearch.length >= 1) {
                    const availableSearch = JSON.parse(JSON.stringify(this.availableSearch));

                    availableSearch.forEach(avail => {
                        if (!this.chosen.includes(avail)) {
                            this.chosen = [...this.chosen, avail];
                        }
                    });

                    this.availableSearch = [];
                }

                this.available = this.available.filter(x => !this.chosen.includes(x));
                this.searchAvailable(null, this.searchAvailableField);
                this.searchChosen(null, this.searchChosenField);
            },
            remove() {
                if (this.chosenSelected.length >= 1) {
                    this.chosenSelected.forEach(chosSelecionada => {
                        this.chosen = this.chosen.filter(x => x !== chosSelecionada);
                    });

                    const chosenSelected = JSON.parse(JSON.stringify(this.chosenSelected));

                    chosenSelected.forEach(chosSelecionada => {
                        if (!this.available.includes(chosSelecionada)) {
                            this.available = [...this.available, chosSelecionada];
                        }
                    });
                }

                this.searchAvailable(null, this.searchAvailableField);
                this.searchChosen(null, this.searchChosenField);
            },
            removeAll() {
                if (this.chosenSearch.length >= 1) {
                    const chosenSearch = JSON.parse(JSON.stringify(this.chosenSearch));

                    chosenSearch.forEach(chos => {
                        if (!this.available.includes(chos)) {
                            this.available = [...this.available, chos];
                        }
                    });

                    this.chosenSearch = [];
                }

                this.chosen = this.chosen.filter(x => !this.available.includes(x));
                this.searchAvailable(null, this.searchAvailableField);
                this.searchChosen(null, this.searchChosenField);
            },
        }
    });

    app.mount(mountNode);
}

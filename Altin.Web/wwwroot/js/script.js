let previousData = {
    gramAlis: null,
    gramSatis: null,
    gramYuzde: null,
    ceyrekAlis: null,
    ceyrekSatis: null,
    ceyrekYuzde: null,
    yarimAlis: null,
    yarimSatis: null,
    yarimYuzde: null,
    yirmiikialis: null,
    yirmiikisatis: null,
    yirmiikiyuzde: null,
    tamalis: null,
    tamsatis: null,
    tamyuzde: null,
    gremsealis: null,
    gremsesatis: null,
    gremsyuzde: null,
    ataalis: null,
    atasatis: null,
    atayuzde: null,
    kulcealis: null,
    kulcesatis: null,
    kulceyuzde: null,
    dolaralis: null,
    dolarsatis: null,
    dolaryuzde: null,
    euroalis: null,
    eurosatis: null,
    euroyuzde: null,
    eurusdalis: null,
    eurusdsatis: null,
    eurusdyuzde: null,
    xauusdalis: null,
    xauusdsatis: null,
    xauusdyuzde: null,
};

function updateElement(id, newValue, previousValue) {
    const element = document.getElementById(id);
    const roundedValue = parseFloat(newValue).toFixed(2);
    element.innerText = roundedValue;

    if (previousValue !== null) {
        const roundedPreviousValue = parseFloat(previousValue).toFixed(2);
        if (roundedValue  > roundedPreviousValue) {
            element.style.backgroundColor = 'rgba(0, 255, 0, 0.5)';
            element.style.color = 'white';
        } else if (roundedValue  < roundedPreviousValue) {
            element.style.backgroundColor = 'rgba(255, 0, 0, 0.65)';
            element.style.color = 'white';
        } else {
            element.style.color = 'black';
            element.style.backgroundColor = 'transparent';
        }
    }
}

function percentageCalculator(id, currentValue) {
    const element = document.getElementById(id);
    element.innerText = "%" + currentValue;

    if (currentValue > 0) {
        element.style.color = 'rgba(0, 255, 0, 0.75)';
    } else if (currentValue < 0) {
        element.style.color = 'red';
    } else {
        element.style.color = 'black';
    }
}

async function fetchData() {
    const response = await fetch('https://maydagold.com/kurlar/webcanli.json');
    const data = await response.json();

    const newData = {
        gramAlis: data[0][0].alis,
        gramSatis: data[0][0].satis,
        gramYuzde: data[0][0].yuzde,
        ceyrekAlis: data[0][3].alis,
        ceyrekSatis: data[0][3].satis,
        ceyrekYuzde: data[0][3].yuzde,
        yarimAlis: data[0][4].alis,
        yarimSatis: data[0][4].satis,
        yarimYuzde: data[0][4].yuzde,
        yirmiikialis: data[0][1].alis,
        yirmiikisatis: data[0][1].satis,
        yirmiikiyuzde: data[0][1].yuzde,
        tamalis: data[0][5].alis,
        tamsatis: data[0][5].satis,
        tamyuzde: data[0][5].yuzde,
        gremsealis: data[0][6].alis,
        gremsesatis: data[0][6].satis,
        gremseyuzde: data[0][6].yuzde,
        ataalis: data[0][7].alis,
        atasatis: data[0][7].satis,
        atayuzde: data[0][7].yuzde,
        kulcealis: data[0][1].alis,
        kulcesatis: data[0][1].satis,
        kulceyuzde: data[0][1].yuzde,
        dolaralis: data[1][2].alis,
        dolarsatis: data[1][2].satis,
        dolaryuzde: data[1][2].yuzde,
        euroalis: data[1][3].alis,
        eurosatis: data[1][3].satis,
        euroyuzde: data[1][3].yuzde,
        eurusdalis: data[1][4].alis,
        eurusdsatis: data[1][4].satis,
        eurusdyuzde: data[1][4].yuzde,
        xauusdalis: data[1][1].alis,
        xauusdsatis: data[1][1].satis,
        xauusdyuzde: data[1][1].yuzde
    };

    updateElement('gramalis', newData.gramAlis, previousData.gramAlis);
    updateElement('gramsatis', newData.gramSatis, previousData.gramSatis);
    percentageCalculator('gramyuzde', newData.gramYuzde);
    updateElement('ceyrekalis', newData.ceyrekAlis, previousData.ceyrekAlis);
    updateElement('ceyreksatis', newData.ceyrekSatis, previousData.ceyrekSatis);
    percentageCalculator('ceyrekyuzde', newData.ceyrekYuzde);
    updateElement('yarimalis', newData.yarimAlis, previousData.yarimAlis);
    updateElement('yarimsatis', newData.yarimSatis, previousData.yarimSatis);
    percentageCalculator('yarimyuzde', newData.yarimYuzde);
    updateElement('yirmiikialis', newData.yirmiikialis, previousData.yirmiikialis);
    updateElement('yirmiikisatis', newData.yirmiikisatis, previousData.yirmiikisatis);
    percentageCalculator('yirmiikiyuzde', newData.yirmiikiyuzde);
    updateElement('tamalis', newData.tamalis, previousData.tamalis);
    updateElement('tamsatis', newData.tamsatis, previousData.tamsatis);
    percentageCalculator('tamyuzde', newData.tamyuzde);
    updateElement('gremsealis', newData.gremsealis, previousData.gremsealis);
    updateElement('gremsesatis', newData.gremsesatis, previousData.gremsesatis);
    percentageCalculator('gremseyuzde', newData.gremseyuzde);
    updateElement('ataalis', newData.ataalis, previousData.ataalis);
    updateElement('atasatis', newData.atasatis, previousData.atasatis);
    percentageCalculator('atayuzde', newData.atayuzde);
    updateElement('kulcealis', newData.kulcealis, previousData.kulcealis);
    updateElement('kulcesatis', newData.kulcesatis, previousData.kulcesatis);
    percentageCalculator('kulceyuzde', newData.kulceyuzde);
    updateElement('dolaralis', newData.dolaralis, previousData.dolaralis);
    updateElement('dolarsatis', newData.dolarsatis, previousData.dolarsatis);
    percentageCalculator('dolaryuzde', newData.dolaryuzde);
    updateElement('euroalis', newData.euroalis, previousData.euroalis);
    updateElement('eurosatis', newData.eurosatis, previousData.eurosatis);
    percentageCalculator('euroyuzde', newData.euroyuzde);
    updateElement('eurusdalis', newData.eurusdalis, previousData.eurusdalis);
    updateElement('eurusdsatis', newData.eurusdsatis, previousData.eurusdsatis);
    percentageCalculator('eurusdyuzde', newData.eurusdyuzde);
    updateElement('xauusdalis', newData.xauusdalis, previousData.xauusdalis);
    updateElement('xauusdsatis', newData.xauusdsatis, previousData.xauusdsatis);
    percentageCalculator('xauusdyuzde', newData.xauusdyuzde);
    previousData = newData;
}

window.onload = () => {
    fetchData();
    setInterval(fetchData, 5000);
};

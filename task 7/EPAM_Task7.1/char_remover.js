function remover(inputText) {
    const separatorSymbols = [' ', '\t', '?', '!', ':', ';', ',', '.']; 
    const separatedText = customSplit(inputText, separatorSymbols);

    let repeatedSymbols = new Set();
    for(let i = 0; i < separatedText.length; i++) {
        repeatedSymbols = union(repeatedSymbols, getRepeatedSymbols(separatedText[i]))
    }

    let resultString = inputText;
    for(let symbol of repeatedSymbols) {
        resultString = resultString.replaceAll(symbol, "");
    }

    return resultString;
}

function customSplit(str, separators) {
    let joinLetter = separators[0];

    for(let i = 1; i < separators.length; i++) {
        str = str.split(separators[i]).join(joinLetter);    
    }

    str = str.split(joinLetter);
    return str;
}

function getRepeatedSymbols(word) {
    let symbols = new Set();

    for(let i = 0; i < word.length; i++) {
        if(i != word.lastIndexOf(word[i])) {
            symbols.add(word[i])
        }
    }
    
    return symbols;
}

function union(setA, setB) {
    let _union = new Set(setA);
    
    for (let elem of setB) {
        _union.add(elem);
    }

    return _union;
}
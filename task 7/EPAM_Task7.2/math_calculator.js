// first statement to proceed '/'
function parseDivideSeparatedExpression(expression) {
    const numbersString = expression.split('/');
	const numbers = numbersString.map(numStr => +numStr);
	const initialValue = Math.pow(numbers[0], 2);
	const result = numbers.reduce((acc, num) => acc / num, initialValue);
	return result;
}

// '/' '*' will be proceed
function parseMultiplicationSeparatedExpression(expression) {
	const numbersString = expression.split('*');
	const numbers = numbersString.map(numStr => parseDivideSeparatedExpression(numStr));
	const initialValue = 1.0;
	const result = numbers.reduce((acc, num) => acc * num, initialValue);
	return result;
};

// '/' '*' '-' will be procced
function parseMinusSeparatedExpression(expression) {
	const numbersString = expression.split('-');
	const numbers = numbersString.map(numStr => parseMultiplicationSeparatedExpression(numStr));
	const initialValue = numbers[0];
	const result = numbers.slice(1).reduce((acc, num) => acc - num, initialValue);
	return result;
};

// '/' '*' '-' '+' will be procced
function parsePlusSeparatedExpression(expression) {
	const numbersString = expression.split('+');
	const numbers = numbersString.map(numStr => parseMinusSeparatedExpression(numStr));
	const initialValue = 0.0;
	const result = numbers.reduce((acc, num) => acc + num, initialValue);
	return result;
};

function split(expression, operator) {
	const result = [];
	let expressionPart = "";

	for (let i = 0; i < expression.length; ++i) {
		const curChar = expression[i];
		if (operator == curChar) {
			result.push(expressionPart);
			expressionPart = "";
		} else {
            expressionPart += curChar;
        }
	}

	if (expressionPart != "") {
		result.push(expressionPart);
	}

	return result;
};

function expressionCheck (expression) {
    let commaCounter = 0;
    let operatorCounter = 0;

    if(/\+|\*|\/|\^/.test(expression[0])) {
        return false;
    }

    for(let i = 0; i < expression.length - 1; i++) {
        if(/\+|\-|\*|\/|\^|\.|\d/.test(expression[i]) == false) {
            return false;
        }

        if(expression[i] === ".") {
            commaCounter++;
        } else {
            commaCounter = 0;
        }

        if(/\+|\-|\*|\/|\^/.test(expression[i])) {
            operatorCounter++;
        } else {
            operatorCounter = 0;
        }

        if(commaCounter > 1) {
            return false;
        }

        if(operatorCounter > 1) {
            return false;
        }
    }

    return true;
};

function parse(expression) {
    if(expression[expression.length - 1] != "=") {
        console.log("there must be letter \"=\" in the end");
        return null;
    }

    const normalizeString = expression.replace(/ /g,'').slice(0, -1);

    if(!expressionCheck(normalizeString)) {
        console.log("can't parse this expression");
        return null;
    }

    const result = parsePlusSeparatedExpression(normalizeString, '+');   
    return result;
};
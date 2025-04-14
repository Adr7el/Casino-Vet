export function useRoulette() {
    const numerosRoulette = [
        0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6,
        27, 13, 36, 11, 30, 8, 23, 10, 5, 24,
        16, 33, 1, 20, 14, 31, 9, 22, 18, 29,
        7, 28, 12, 35, 3, 26
    ]

    const getColor = (num) => {
        if (num === 0) return 'green'
        const numerosRojos = new Set([
            1, 3, 5, 7, 9, 12, 14, 16, 18, 19,
            21, 23, 25, 27, 30, 32, 34, 36
        ])
        return numerosRojos.has(num) ? 'red' : 'black'
    }

    const items = numerosRoulette.map((num) => ({
        id: num,
        name: num.toString(),
        htmlContent: num.toString(),
        background: getColor(num)
    }))

    return {
        items,
        getColor
    }
}

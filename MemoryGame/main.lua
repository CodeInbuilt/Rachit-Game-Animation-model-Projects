function love.load()

    cards = {
        {value = "A"}, {value = "A"},
        {value = "B"}, {value = "B"},
        {value = "C"}, {value = "C"},
        {value = "D"}, {value = "D"}
    }

    shuffle(cards)

    firstCard = nil
    secondCard = nil

    lockBoard = false

    for i, card in ipairs(cards) do
        card.revealed = false
        card.matched = false

        card.x = ((i - 1) % 4) * 110 + 100
        card.y = math.floor((i - 1) / 4) * 150 + 100

        card.width = 100
        card.height = 140
    end
end

function shuffle(t)
    for i = #t, 2, -1 do
        local j = math.random(i)
        t[i], t[j] = t[j], t[i]
    end
end

function love.mousepressed(x, y, button)

    if button == 1 and not lockBoard then

        for i, card in ipairs(cards) do

            if not card.revealed and not card.matched and
               x > card.x and x < card.x + card.width and
               y > card.y and y < card.y + card.height then

                card.revealed = true

                if firstCard == nil then
                    firstCard = card

                elseif secondCard == nil then
                    secondCard = card
                    lockBoard = true
                end
            end
        end
    end
end

function love.update(dt)

    if firstCard and secondCard then

        if firstCard.value == secondCard.value then

            firstCard.matched = true
            secondCard.matched = true

        else

            if not timer then
                timer = 1
            end

            timer = timer - dt

            if timer <= 0 then
                firstCard.revealed = false
                secondCard.revealed = false
                timer = nil
            else
                return
            end
        end

        firstCard = nil
        secondCard = nil
        lockBoard = false
    end
end

function love.draw()

    love.graphics.print("Memory Card Matching Game", 220, 30)

    for i, card in ipairs(cards) do

        love.graphics.rectangle(
            "line",
            card.x,
            card.y,
            card.width,
            card.height
        )

        if card.revealed or card.matched then

            love.graphics.print(
                card.value,
                card.x + 40,
                card.y + 55
            )

        else

            love.graphics.print(
                "?",
                card.x + 40,
                card.y + 55
            )
        end
    end
end
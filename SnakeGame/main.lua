function love.load()

    cellSize = 20
    cols = 30
    rows = 20

    snake = {
        body = {
            {x = 10, y = 10}
        },

        dirX = 1,
        dirY = 0
    }

    food = {
        x = math.random(0, cols - 1),
        y = math.random(0, rows - 1)
    }

    timer = 0
    speed = 0.15

    score = 0
end

function love.update(dt)

    timer = timer + dt

    if timer >= speed then
        timer = 0

        local head = snake.body[1]

        local newHead = {
            x = head.x + snake.dirX,
            y = head.y + snake.dirY
        }

        -- Wall collision
        if newHead.x < 0 or newHead.x >= cols or
           newHead.y < 0 or newHead.y >= rows then

            love.load()
            return
        end

        -- Self collision
        for i, part in ipairs(snake.body) do
            if newHead.x == part.x and newHead.y == part.y then
                love.load()
                return
            end
        end

        table.insert(snake.body, 1, newHead)

        -- Food collision
        if newHead.x == food.x and newHead.y == food.y then

            score = score + 1

            food.x = math.random(0, cols - 1)
            food.y = math.random(0, rows - 1)

        else
            table.remove(snake.body)
        end
    end
end

function love.keypressed(key)

    if key == "up" and snake.dirY == 0 then
        snake.dirX = 0
        snake.dirY = -1

    elseif key == "down" and snake.dirY == 0 then
        snake.dirX = 0
        snake.dirY = 1

    elseif key == "left" and snake.dirX == 0 then
        snake.dirX = -1
        snake.dirY = 0

    elseif key == "right" and snake.dirX == 0 then
        snake.dirX = 1
        snake.dirY = 0
    end
end

function love.draw()

    -- Draw snake
    for i, part in ipairs(snake.body) do
        love.graphics.rectangle(
            "fill",
            part.x * cellSize,
            part.y * cellSize,
            cellSize,
            cellSize
        )
    end

    -- Draw food
    love.graphics.rectangle(
        "fill",
        food.x * cellSize,
        food.y * cellSize,
        cellSize,
        cellSize
    )

    -- Score
    love.graphics.print("Score: " .. score, 10, 10)
end
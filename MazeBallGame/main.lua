function love.load()

    ball = {
        x = 50,
        y = 50,
        radius = 15,
        speed = 220
    }

    goal = {
        x = 700,
        y = 500,
        width = 50,
        height = 50
    }

    walls = {

        {x = 150, y = 0, width = 20, height = 400},
        {x = 300, y = 200, width = 20, height = 400},
        {x = 450, y = 0, width = 20, height = 400},
        {x = 600, y = 200, width = 20, height = 400}
    }

    message = "Reach the Green Goal!"
end

function love.update(dt)

    local oldX = ball.x
    local oldY = ball.y

    -- Movement Physics
    if love.keyboard.isDown("up") then
        ball.y = ball.y - ball.speed * dt
    end

    if love.keyboard.isDown("down") then
        ball.y = ball.y + ball.speed * dt
    end

    if love.keyboard.isDown("left") then
        ball.x = ball.x - ball.speed * dt
    end

    if love.keyboard.isDown("right") then
        ball.x = ball.x + ball.speed * dt
    end

    -- Wall Collision
    for i, wall in ipairs(walls) do

        if ball.x + ball.radius > wall.x and
           ball.x - ball.radius < wall.x + wall.width and
           ball.y + ball.radius > wall.y and
           ball.y - ball.radius < wall.y + wall.height then

            ball.x = oldX
            ball.y = oldY
        end
    end

    -- Goal Collision
    if ball.x + ball.radius > goal.x and
       ball.x - ball.radius < goal.x + goal.width and
       ball.y + ball.radius > goal.y and
       ball.y - ball.radius < goal.y + goal.height then

        message = "You Win!"
    end
end

function love.draw()

    -- Draw Ball
    love.graphics.circle(
        "fill",
        ball.x,
        ball.y,
        ball.radius
    )

    -- Draw Walls
    for i, wall in ipairs(walls) do

        love.graphics.rectangle(
            "fill",
            wall.x,
            wall.y,
            wall.width,
            wall.height
        )
    end

    -- Draw Goal
    love.graphics.rectangle(
        "fill",
        goal.x,
        goal.y,
        goal.width,
        goal.height
    )

    -- Text
    love.graphics.print(message, 20, 20)

    love.graphics.print(
        "Use Arrow Keys to Move",
        20,
        50
    )
end
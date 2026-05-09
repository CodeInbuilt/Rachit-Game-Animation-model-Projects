function love.load()

    -- Paddle
    paddle = {
        x = 30,
        y = 250,
        width = 20,
        height = 100,
        speed = 300
    }

    -- Ball
    ball = {
        x = 400,
        y = 300,
        radius = 10,
        speedX = 250,
        speedY = 200
    }

    score = 0
end

function love.update(dt)

    -- Paddle Movement
    if love.keyboard.isDown("up") then
        paddle.y = paddle.y - paddle.speed * dt
    end

    if love.keyboard.isDown("down") then
        paddle.y = paddle.y + paddle.speed * dt
    end

    -- Ball Movement
    ball.x = ball.x + ball.speedX * dt
    ball.y = ball.y + ball.speedY * dt

    -- Top and Bottom Wall Collision
    if ball.y < 0 then
        ball.y = 0
        ball.speedY = -ball.speedY
    end

    if ball.y > love.graphics.getHeight() - ball.radius then
        ball.y = love.graphics.getHeight() - ball.radius
        ball.speedY = -ball.speedY
    end

    -- Paddle Collision
    if ball.x < paddle.x + paddle.width and
       ball.y < paddle.y + paddle.height and
       ball.y + ball.radius > paddle.y then

        ball.speedX = -ball.speedX
        ball.x = paddle.x + paddle.width

        score = score + 1
    end

    -- Reset Ball if Missed
    if ball.x < 0 then
        ball.x = 400
        ball.y = 300

        ball.speedX = 250
        ball.speedY = 200

        score = 0
    end

    -- Right Wall Bounce
    if ball.x > love.graphics.getWidth() - ball.radius then
        ball.speedX = -ball.speedX
    end
end

function love.draw()

    -- Draw Paddle
    love.graphics.rectangle(
        "fill",
        paddle.x,
        paddle.y,
        paddle.width,
        paddle.height
    )

    -- Draw Ball
    love.graphics.circle(
        "fill",
        ball.x,
        ball.y,
        ball.radius
    )

    -- Score
    love.graphics.print("Score: " .. score, 10, 10)

    -- Controls
    love.graphics.print("Use UP and DOWN Arrow Keys", 250, 10)
end
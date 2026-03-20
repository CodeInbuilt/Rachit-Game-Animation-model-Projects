player = {
    x = 300,
    y = 200,
    size = 50,
    speed = 200
}

function love.update(dt)
    if love.keyboard.isDown("right") then
        player.x = player.x + player.speed * dt
    end

    if love.keyboard.isDown("left") then
        player.x = player.x - player.speed * dt
    end

    if love.keyboard.isDown("up") then
        player.y = player.y - player.speed * dt
    end

    if love.keyboard.isDown("down") then
        player.y = player.y + player.speed * dt
    end
end

function love.draw()
    love.graphics.setColor(0, 1, 0)
    love.graphics.rectangle("fill", player.x, player.y, player.size, player.size)
end
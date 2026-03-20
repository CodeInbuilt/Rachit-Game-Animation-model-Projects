enemies = {}
spawnTimer = 0
spawnInterval = 2 -- seconds

-- Spawn area
spawnArea = {
    x = 50,
    y = 50,
    width = 700,
    height = 500
}

-- Function to spawn enemy
function spawnEnemy()
    local enemy = {
        x = love.math.random(spawnArea.x, spawnArea.x + spawnArea.width),
        y = love.math.random(spawnArea.y, spawnArea.y + spawnArea.height),
        size = 20
    }
    table.insert(enemies, enemy)
end

-- Update function
function love.update(dt)
    spawnTimer = spawnTimer + dt

    if spawnTimer >= spawnInterval then
        spawnEnemy()
        spawnTimer = 0
    end
end

-- Draw enemies
function love.draw()
    -- Draw spawn area (for reference)
    love.graphics.rectangle("line", spawnArea.x, spawnArea.y, spawnArea.width, spawnArea.height)

    -- Draw enemies
    for i, enemy in ipairs(enemies) do
        love.graphics.circle("fill", enemy.x, enemy.y, enemy.size)
    end
end
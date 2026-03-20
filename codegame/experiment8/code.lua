Drawable = {}

function Drawable:draw()
    error("draw() not implemented!")
end

function Drawable:update(dt)
    error("update() not implemented!")
end
Player = {}
Player.__index = Player

function Player:new(x, y)
    local obj = {
        x = x,
        y = y,
        speed = 100
    }
    setmetatable(obj, self)
    return obj
end

function Player:update(dt)
    if love.keyboard.isDown("right") then
        self.x = self.x + self.speed * dt
    end
end

function Player:draw()
    love.graphics.setColor(0, 1, 0) -- green
    love.graphics.rectangle("fill", self.x, self.y, 30, 30)
end
Enemy = {}
Enemy.__index = Enemy

function Enemy:new(x, y)
    local obj = {
        x = x,
        y = y,
        speed = 50
    }
    setmetatable(obj, self)
    return obj
end

function Enemy:update(dt)
    self.y = self.y + self.speed * dt
end

function Enemy:draw()
    love.graphics.setColor(1, 0, 0) -- red
    love.graphics.circle("fill", self.x, self.y, 15)
end
objects = {}

function love.load()
    local player = Player:new(100, 100)
    local enemy = Enemy:new(200, 50)

    table.insert(objects, player)
    table.insert(objects, enemy)
end

function love.update(dt)
    for i, obj in ipairs(objects) do
        obj:update(dt) -- same method (interface)
    end
end

function love.draw()
    for i, obj in ipairs(objects) do
        obj:draw() -- same method (interface)
    end
end
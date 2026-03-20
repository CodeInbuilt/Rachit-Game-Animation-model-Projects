-- Game object properties
object = {
    x = 300,
    y = 200,
    width = 100,
    height = 100,
    color = {1, 0, 0} -- red
}

-- Draw function
function love.draw()
    love.graphics.setColor(object.color)
    love.graphics.rectangle("fill", object.x, object.y, object.width, object.height)
end

-- Mouse click event
function love.mousepressed(mx, my, button)
    if button == 1 then -- left click
        -- check if click is inside rectangle
        if mx > object.x and mx < object.x + object.width and
           my > object.y and my < object.y + object.height then
            
            -- change to random color
            object.color = {math.random(), math.random(), math.random()}
        end
    end
end

-- Load function
function love.load()
    love.window.setTitle("LÖVE2D Demo")
end
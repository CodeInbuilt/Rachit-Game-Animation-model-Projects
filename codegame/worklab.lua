-- Object positions (x, y)
object1 = {x = 2, y = 3}
object2 = {x = 7, y = 9}

-- Function to calculate distance
function calculateDistance(obj1, obj2)
    local dx = obj2.x - obj1.x
    local dy = obj2.y - obj1.y
    local distance = math.sqrt(dx * dx + dy * dy)
    return distance
end

-- Calculate distance
local result = calculateDistance(object1, object2)

-- Display result
print("Distance between objects: " .. result)
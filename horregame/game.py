import pygame
import sys
import random

# Initialize
pygame.init()

# Fullscreen
screen = pygame.display.set_mode((0, 0), pygame.FULLSCREEN)
screen_width, screen_height = screen.get_size()
pygame.display.set_caption("Haunted Escape")

# Load background
background = pygame.image.load("dark_forest.jpg")
background = pygame.transform.scale(background, (screen_width, screen_height))

# Sounds
pygame.mixer.music.load("sound-ambience-sonido-ambiente-3-14040.mp3")
pygame.mixer.music.play(-1)
jumpscare_sound = pygame.mixer.Sound("jumpscare-94984.mp3")

# Load jumpscare images
jumpscare_img1 = pygame.image.load("gh.jpg")
jumpscare_img1 = pygame.transform.scale(jumpscare_img1, (screen_width, screen_height))
jumpscare_img2 = pygame.image.load("ghost women.jpg")
jumpscare_img2 = pygame.transform.scale(jumpscare_img2, (screen_width, screen_height))

# Font
font = pygame.font.SysFont(None, 80)
puzzle_font = pygame.font.SysFont(None, 50)

# Player setup
player = pygame.Rect(screen_width//2, screen_height//2, 40, 40)
player_color = (255, 255, 255)
player_speed = 5

# Ghosts
ghosts = [
    pygame.Rect(random.randint(0, screen_width), random.randint(0, screen_height), 60, 60),
    pygame.Rect(random.randint(0, screen_width), random.randint(0, screen_height), 60, 60)
]
ghost_color = (255, 0, 0)
ghost_speed = 1.5

# Jumpscare zones
jumpscare_zones = [pygame.Rect(x, y, 60, 60) for x, y in [(200, 300), (600, 800), (screen_width//2, screen_height//2)]]

# Key setup
key_img = pygame.image.load("key pixel.png")
key = key_img.get_rect(topleft=(random.randint(100, screen_width - 100), random.randint(100, screen_height - 100)))
key_collected = False

# Book setup
book_img = pygame.image.load("kook.jpeg")
book = book_img.get_rect(topleft=(random.randint(100, screen_width - 100), random.randint(100, screen_height - 100)))
book_collected = False

# Exit door
door_img = pygame.image.load("images.png")
door = door_img.get_rect(topleft=(screen_width - 150, screen_height - 150))
door_visible = False

# Power-up (invisibility)
powerup_img = pygame.image.load("invisible man .png")
powerup = powerup_img.get_rect(topleft=(random.randint(100, screen_width - 100), random.randint(100, screen_height - 100)))
powerup_visible = True
invisible = False
invisible_timer = 0

# Health pack setup (new element)
healthpack_img = pygame.image.load("792.png")
healthpack = healthpack_img.get_rect(topleft=(random.randint(100, screen_width - 100), random.randint(100, screen_height - 100)))
healthpack_visible = True

# Game clock
clock = pygame.time.Clock()

# Game states
alive = True
escaped = False
jumpscare_played = False
selected_jumpscare = random.choice([jumpscare_img1, jumpscare_img2])
lives = 3

# Puzzle state
puzzle_mode = False
puzzle_solved = False
input_text = ""
puzzle_start_time = None

# Riddles
riddles = [
    ("What walks on four legs in the morning, two legs at noon, and three in the evening?", "man"),
    ("I speak without a mouth and hear without ears. I have no body, but I come alive with the wind. What am I?", "echo"),
    ("The more of me you take, the more you leave behind. What am I?", "footsteps"),
    ("What has to be broken before you can use it?", "egg"),
    ("I’m tall when I’m young, and I’m short when I’m old. What am I?", "candle")
]
current_riddle, current_answer = random.choice(riddles)

# Game loop
while True:
    screen.blit(background, (0, 0))

    for event in pygame.event.get():
        if event.type == pygame.QUIT or (event.type == pygame.KEYDOWN and event.key == pygame.K_ESCAPE):
            pygame.quit()
            sys.exit()

        if puzzle_mode:
            if event.type == pygame.KEYDOWN:
                if event.key == pygame.K_RETURN:
                    if input_text.lower().strip() == current_answer:
                        puzzle_solved = True
                        puzzle_mode = False
                    else:
                        input_text = ""
                        book_collected = False
                        book.topleft = (random.randint(100, screen_width - 100), random.randint(100, screen_height - 100))
                elif event.key == pygame.K_BACKSPACE:
                    input_text = input_text[:-1]
                else:
                    input_text += event.unicode

    if puzzle_mode:
        if puzzle_start_time is None:
            puzzle_start_time = pygame.time.get_ticks()

        for i, line in enumerate(current_riddle.split('\n')):
            riddle_render = puzzle_font.render("RIDDLE: " + line, True, (255, 255, 255))
            screen.blit(riddle_render, (screen_width//2 - 400, 200 + i * 60))
        input_render = puzzle_font.render("Your Answer: " + input_text, True, (0, 255, 0))
        screen.blit(input_render, (screen_width//2 - 400, 400))

        time_left = 30 - (pygame.time.get_ticks() - puzzle_start_time) // 1000
        timer_render = puzzle_font.render(f"Time Left: {time_left}", True, (255, 0, 0))
        screen.blit(timer_render, (screen_width//2 - 400, 470))

        if time_left <= 0:
            alive = False
            puzzle_mode = False

        pygame.display.update()
        continue

    if alive and not escaped:
        keys = pygame.key.get_pressed()
        if keys[pygame.K_LEFT]: player.x -= player_speed
        if keys[pygame.K_RIGHT]: player.x += player_speed
        if keys[pygame.K_UP]: player.y -= player_speed
        if keys[pygame.K_DOWN]: player.y += player_speed

        if invisible and pygame.time.get_ticks() - invisible_timer > 5000:
            invisible = False

        if not invisible:
            for ghost in ghosts:
                if ghost.x < player.x: ghost.x += ghost_speed
                if ghost.x > player.x: ghost.x -= ghost_speed
                if ghost.y < player.y: ghost.y += ghost_speed
                if ghost.y > player.y: ghost.y -= ghost_speed

        pygame.draw.rect(screen, player_color, player)
        for ghost in ghosts:
            pygame.draw.rect(screen, ghost_color, ghost)

        if not key_collected:
            screen.blit(key_img, key)
        if not book_collected:
            screen.blit(book_img, book)
        if key_collected:
            door_visible = True
            screen.blit(door_img, door)
        if powerup_visible:
            screen.blit(powerup_img, powerup)
        if healthpack_visible:
            screen.blit(healthpack_img, healthpack)

        if player.colliderect(key):
            key_collected = True
        if player.colliderect(book) and not book_collected:
            book_collected = True
            puzzle_mode = True
            input_text = ""
            puzzle_solved = False
            puzzle_start_time = None
            current_riddle, current_answer = random.choice(riddles)
        if player.colliderect(powerup) and powerup_visible:
            invisible = True
            invisible_timer = pygame.time.get_ticks()
            powerup_visible = False
        if player.colliderect(healthpack) and healthpack_visible:
            if lives < 3:
                lives += 1
            healthpack_visible = False

        if door_visible and player.colliderect(door) and puzzle_solved:
            escaped = True

        if not invisible and any(player.colliderect(g) for g in ghosts):
            if lives > 1:
                lives -= 1
                player.topleft = (screen_width // 2, screen_height // 2)
                pygame.time.delay(1000)
            else:
                if not jumpscare_played:
                    jumpscare_sound.play()
                    jumpscare_played = True
                alive = False

        for zone in jumpscare_zones:
            if player.colliderect(zone) and not jumpscare_played:
                screen.blit(selected_jumpscare, (0, 0))
                jumpscare_sound.play()
                pygame.display.update()
                pygame.time.delay(1000)
                jumpscare_played = True

        if pygame.time.get_ticks() % 10000 < 50:
            new_ghost = pygame.Rect(player.x + random.randint(-100, 100), player.y + random.randint(-100, 100), 60, 60)
            ghosts.append(new_ghost)

        if random.randint(0, 500) == 0:
            hallucination = font.render("RUN...", True, (255, 0, 0))
            screen.blit(hallucination, (random.randint(100, screen_width-300), random.randint(100, screen_height-100)))

        lives_text = font.render(f"Lives: {lives}", True, (255, 255, 0))
        screen.blit(lives_text, (30, 30))
        if invisible:
            inv_text = font.render("Invisible!", True, (0, 255, 255))
            screen.blit(inv_text, (30, 100))

        # Flashlight effect
        dark_overlay = pygame.Surface((screen_width, screen_height))
        dark_overlay.fill((0, 0, 0))
        dark_overlay.set_alpha(200)
        flashlight_radius = 150 if random.random() > 0.01 else random.choice([0, 50, 150])
        pygame.draw.circle(dark_overlay, (0, 0, 0, 0), player.center, flashlight_radius)
        screen.blit(dark_overlay, (0, 0))

    elif not alive:
        screen.blit(selected_jumpscare, (0, 0))
        text = font.render("YOU ARE DEAD", True, (255, 0, 0))
        screen.blit(text, (screen_width // 2 - text.get_width() // 2, screen_height - 100))

    elif escaped:
        text = font.render("YOU ESCAPED!", True, (0, 255, 0))
        screen.blit(text, (screen_width // 2 - text.get_width() // 2, screen_height // 2))

    pygame.display.update()
    clock.tick(60)
